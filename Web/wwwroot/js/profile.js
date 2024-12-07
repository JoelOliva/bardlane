import { getFormattedTime } from "./site.js";

let currentPackage = document.getElementById("basic");
let currentPackageTab = document.getElementById("basicBtn");
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

const basicBtn = document.getElementById("basicBtn");
const standardBtn = document.getElementById("standardBtn");
const premiumBtn = document.getElementById("premiumBtn");
basicBtn.addEventListener("click", () => { changePackage("basic"); });
standardBtn.addEventListener("click", () => { changePackage("standard"); });
premiumBtn.addEventListener("click", () => { changePackage("premium"); });

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

function displayAudioDuration() {
    for (let i = 0; i < tracks.length; i++) {
        durations[i].textContent = getFormattedTime(tracks[i].duration);
    }
}

function audioHasLoaded() {
    for (let i = 0; i < tracks.length; i++) {
        if (isNaN(tracks[i].duration)) return;
    }

    clearInterval(interval);
    displayAudioDuration();
}

function changePackage(packageType) {
    const packageContainer = document.getElementById(packageType);
    packageContainer.style.display = "block";
    currentPackage.style.display = "none";
    currentPackage = packageContainer;

    const packageTab = document.getElementById(packageType + "Btn");
    packageTab.classList.remove("btn-secondary");
    packageTab.classList.add("btn-warning");
    currentPackageTab.classList.remove("btn-warning");
    currentPackageTab.classList.add("btn-secondary");
    currentPackageTab = packageTab;
}
