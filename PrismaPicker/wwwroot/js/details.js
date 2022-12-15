// This script allows for dynamic color assignment on Glass/Details/[ID] pages
window.onload = function() {
    let colorString = document.getElementById("colorString").innerHTML;
    let sampleBar = document.getElementById("sampleColorBar");
    let sampleBlock = document.getElementById("sampleColorBlock");

    sampleBar.style.color = colorString;
    sampleBar.style.backgroundColor = colorString;
    sampleBlock.style.borderColor = colorString;
}
