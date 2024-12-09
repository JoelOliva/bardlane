import { getFormattedTime } from "./site.js";

const player = document.getElementById("player");
player.classList.remove("d-none");
player.classList.add("d-flex");
const interval = setInterval(audioHasLoaded, 100);
const tracks = document.getElementsByClassName("track");
const durations = document.getElementsByClassName("duration");

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
