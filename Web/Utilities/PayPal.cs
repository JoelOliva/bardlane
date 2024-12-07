using PaypalServerSdk.Standard;
using PaypalServerSdk.Standard.Authentication;
using PaypalServerSdk.Standard.Exceptions;
using PaypalServerSdk.Standard.Http.Response;
using PaypalServerSdk.Standard.Models;

namespace Web.Utilities
{
	public class PayPal
	{
		private readonly PaypalServerSdkClient _client;

		public PayPal(string clientId, string clientSecret)
		{
			_client = new PaypalServerSdkClient.Builder()
				.ClientCredentialsAuth(
					new ClientCredentialsAuthModel.Builder(clientId, clientSecret)
					.Build()
				)
				.Environment(PaypalServerSdk.Standard.Environment.Sandbox)
				.LoggingConfig(config => config
					.LogLevel(LogLevel.Information)
					.RequestConfig(reqConfig => reqConfig.Body(true))
					.ResponseConfig(respConfig => respConfig.Headers(true))
				)
				.Build();
		}

		public async Task<Order?> CreateOrder(string amount)
		{

			OrdersCreateInput ordersCreateInput = new OrdersCreateInput
			{
				Body = new OrderRequest
				{
					Intent = CheckoutPaymentIntent.Capture,
					PurchaseUnits = new List<PurchaseUnitRequest>
					{
						new PurchaseUnitRequest
						{
							Amount = new AmountWithBreakdown
							{
								CurrencyCode = "USD",
								MValue = amount,
							},
						},
					},
				},
				Prefer = "return=minimal",
			};

			try
			{
				ApiResponse<Order> result = await _client.OrdersController.OrdersCreateAsync(ordersCreateInput);
				return result.Data;
			}
			catch (ApiException e)
			{
				Console.WriteLine(e.Message);
			}

			return null;
		}

		public async Task<Order?> CaptureOrder(string orderId)
		{

			OrdersCaptureInput ordersCaptureInput = new OrdersCaptureInput
			{
				Id = orderId,
				Prefer = "return=minimal",
			};

			try
			{
				ApiResponse<Order> result = await _client.OrdersController.OrdersCaptureAsync(ordersCaptureInput);
				return result.Data;
			}
			catch (ApiException e)
			{
				Console.WriteLine(e.Message);
			}

			return null;
		}
	}
}
