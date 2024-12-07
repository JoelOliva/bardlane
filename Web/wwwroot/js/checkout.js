window.paypal
    .Buttons({
        style: {
            shape: "rect",
            layout: "vertical",
            color: "gold",
            label: "paypal",
        },
        async createOrder() {
            const queryString = window.location.search;
            const params = new URLSearchParams(queryString);
            const packageId = params.get("PackageId");
            try {
                const response = await fetch("/Checkout?PackageId=" + packageId, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: { packageId: packageId }
                });

                const orderData = await response.json();

                if (orderData.id) {
                    return orderData.id;
                }
                const errorDetail = orderData?.details?.[0];
                const errorMessage = errorDetail
                    ? `${errorDetail.issue} ${errorDetail.description} (${orderData.debug_id})`
                    : JSON.stringify(orderData);

                throw new Error(errorMessage);
            } catch (error) {
                console.error(error);
            }
        },

        async onApprove(data, actions) {
            try {
                const response = await fetch(
                    `/Capture/${data.orderID}`,
                    {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json",
                        },
                    }
                );

                const orderData = await response.json();
                const errorDetail = orderData?.details?.[0];

                if (errorDetail?.issue === "INSTRUMENT_DECLINED") {
                    return actions.restart();
                } else if (errorDetail) {
                    // Other non-recoverable errors -> Show a failure message
                    throw new Error(
                        `${errorDetail.description} (${orderData.debug_id})`
                    );
                } else {
                    // Successful transaction, redirect
                    const transaction =
                        orderData?.purchase_units?.[0]?.payments
                            ?.captures?.[0] ||
                        orderData?.purchase_units?.[0]?.payments
                            ?.authorizations?.[0];
                    console.log(
                        "Capture result",
                        orderData,
                        JSON.stringify(orderData, null, 2)
                    );
                    actions.redirect("http://localhost:5148/Success");
                }
            } catch (error) {
                console.error(error);
            }
        },
    })
    .render("#paypal-button-container"); 
