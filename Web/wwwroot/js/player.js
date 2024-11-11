import { getFormattedTime } from "./site.js";

let isPlaying = false;
let previousAudioElement = null;
let previousPlayButton = null;
const timeText = document.getElementById("time");
const durationText = document.getElementById("duration");
const progressBar = document.getElementById("progress-bar");
const mainPlayButton = document.getElementById("main-play-button");
const username = document.getElementById("username");
const trackTitle = document.getElementById("track-title");
const trackProducer = document.getElementById("track-producer");
const playButtons = document.querySelectorAll("#playlist button");
const pauseIcon = '<svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 20 20"><rect width="6" height="16" x="3" y="2" fill="grey" rx="1"/><rect width="6" height="16" x="11" y="2" fill="grey" rx="1"/></svg>'
const mainPauseIcon = '<svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 20 20"><rect width="6" height="16" x="3" y="2" fill="white" rx="1"/><rect width="6" height="16" x="11" y="2" fill="white" rx="1"/></svg>'
const playIcon = '<svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24"><path fill="grey" d="M19.105 11.446a2.34 2.34 0 0 1-.21 1c-.15.332-.38.62-.67.84l-9.65 7.51a2.3 2.3 0 0 1-1.17.46h-.23a2.2 2.2 0 0 1-1-.24a2.29 2.29 0 0 1-1.28-2v-14a2.2 2.2 0 0 1 .33-1.17a2.27 2.27 0 0 1 2.05-1.1c.412.02.812.148 1.16.37l9.66 6.44c.294.204.54.47.72.78c.19.34.29.721.29 1.11" /></svg>'
const mainPlayIcon = mainPlayButton.innerHTML;

let trackDetails = [];
let progressInterval = null;

for (const button of playButtons) {
    button.addEventListener("click", updatePlayer);

    if (username != null)
        trackDetails.push({ "title": tracks[button.id], "producer": username.value });
}
mainPlayButton.addEventListener("click", updatePlayer);

function togglePlay(button) {
    const audioElement = button.nextElementSibling;

    if (isPlaying) {
        // Reset previous audio element and play the new one
        if (audioElement != previousAudioElement) {
            previousAudioElement.pause()
            previousAudioElement.currentTime = 0;
            previousPlayButton.innerHTML = playIcon;
            audioElement.play()
            button.innerHTML = pauseIcon;
            mainPlayButton.innerHTML = mainPauseIcon;
            progressInterval = setInterval(updateProgressBar, 1000);
        }
        // The same play button was pressed so pause audio
        else {
            audioElement.pause();
            isPlaying = false;
            button.innerHTML = playIcon;
            mainPlayButton.innerHTML = mainPlayIcon;
            clearInterval(progressInterval);
        }
    }
    else {
        // Reset previous audio element when audio was paused and a new play button is pressed
        if (audioElement != previousAudioElement && previousAudioElement != null) {
            previousAudioElement.currentTime = 0;
            previousPlayButton.innerHTML = playIcon;
        }
        audioElement.play();
        isPlaying = true;togglePlay
        button.innerHTML = pauseIcon;
        mainPlayButton.innerHTML = mainPauseIcon;
        progressInterval = setInterval(updateProgressBar, 1000);
    }

    previousAudioElement = audioElement;
    previousPlayButton = button;
    trackTitle.innerHTML = trackDetails[button.id].title;
    trackProducer.innerHTML = trackDetails[button.id].producer;
    durationText.textContent = getFormattedTime(audioElement.duration);
}

function updatePlayer() {
    if (this == mainPlayButton && previousPlayButton != null) {
        if (isPlaying) mainPlayButton.innerHTML = pauseIcon;
        else mainPlayButton.innerHTML = playIcon;
        togglePlay(previousPlayButton);
    }
    else if (this != mainPlayButton) {
        togglePlay(this);
    }
}

function updateProgressBar() {
    progressBar.style.width = Math.ceil(previousAudioElement.currentTime / previousAudioElement.duration * 100) + "%";
    timeText.textContent = getFormattedTime(previousAudioElement.currentTime);
}
