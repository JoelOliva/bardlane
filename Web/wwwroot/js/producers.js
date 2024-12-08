const genres = document.querySelectorAll("#genre input");
const producerContainer = document.getElementById("producer");
const genreContainer = document.getElementById("genre");
genreContainer.addEventListener("click", filter);

async function filter() {
    const selectedGenres = [];
    for (const genre of genres) {
        if (genre.checked)
            selectedGenres.push(genre.id);
    }

    const data = new FormData()
    data.append("selectedGenres", selectedGenres);
    const response = await fetch("/Producers", {
        method: "POST",
        body: data
    });

    const producers = await response.json();
    producerContainer.innerHTML = "";

    for (const producer of producers) {
        producerContainer.innerHTML += `
			<a class="nav-link text-dark" asp-page="/Profile" href="/Profile/${producer.userName}">
				<div class="mb-3 text-center">
					<img class="img-fluid mb-3" src="${producer.picturePath}" alt="Portrait" />
					<div class="fw-bold">${producer.userName}</div>
				</div>
			</a>
        `;
    }
}