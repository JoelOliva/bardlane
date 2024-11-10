export function getFormattedTime(seconds) {
    let time = "";
    const minutes = Math.trunc(seconds / 60);
    seconds = Math.trunc(seconds % 60);

    if (minutes > 9)
        time = minutes;
    else
        time = "0" + minutes;

    if (seconds > 9)
        time += ":" + seconds;
    else
        time += ":0" + seconds;

    return time;
}
