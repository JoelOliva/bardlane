const tracks = document.getElementsByClassName("track");
const durations = document.getElementsByClassName("duration");
const username = document.getElementById("username");
const overlay = document.getElementById("overlay");
const closeOverlayBtn = document.getElementById("close-overlay");
const addTrackBtn = document.getElementById("add-track");
const uploadTrackBtn = document.getElementById("upload-track");
closeOverlayBtn.addEventListener("click", closeOverlay);
addTrackBtn.addEventListener("click", showAddTrackOverlay);
uploadTrackBtn.addEventListener("click", uploadTrack);

const interval = setInterval(audioHasLoaded, 100);

function showAddTrackOverlay() {
    overlay.style.visibility = "visible";
}

function closeOverlay() {
    overlay.style.visibility = "hidden";
    resetUploadForm();
}

async function uploadTrack() {
    const titleInput = document.getElementById("title");
    const fileInput = document.querySelector('input[type="file"]');
    const file = fileInput.files[0];

    let isValidInput = true;
    if (titleInput.value.length == 0) {
        titleInput.style.outline = "2px solid red";
        isValidInput = false;
    }
    if (file == undefined) {
        fileInput.style.outline = "2px solid red";
        isValidInput = false;
    }
    if (isValidInput) {
        const formData = new FormData();
        formData.append("file", file);
        formData.append("title", titleInput.value);

        await fetch(`/Profile/${username}?handler=Upload`, {
            method: "POST",
            body: formData
        });

        overlay.style.visibility = "hidden";
        resetUploadForm();
    }
}

function resetUploadForm() {
    const titleInput = document.getElementById("title");
    const fileInput = document.querySelector('input[type="file"]');
    fileInput.value = "";
    titleInput.value = "";
    fileInput.style.outline = "";
    titleInput.style.outline = "";
}

function getFormattedDuration(seconds) {
    const minutes = Math.trunc(seconds / 60);
    seconds = Math.trunc(seconds % 60);

    return minutes + ":" + seconds;
}

function displayAudioDuration() {
    for (let i = 0; i < tracks.length; i++) {
        durations[i].textContent = getFormattedDuration(tracks[i].duration);
    }
}

function audioHasLoaded() {
    for (let i = 0; i < tracks.length; i++) {
        if (isNaN(tracks[i].duration)) return;
    }

    clearInterval(interval);
    displayAudioDuration();
}
