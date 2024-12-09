const fileInput = document.getElementById("file");
const form = document.getElementById("profile-form");
const img = document.getElementById("avatar");
img.addEventListener("click", triggerFileDialog);
form.addEventListener("submit", save);

function triggerFileDialog() {
    fileInput.click();
}

async function save(event) {
    const file = fileInput.files[0];
    console.log(file);

    if (file != undefined) {
        form.appendChild(fileInput);
    }
}