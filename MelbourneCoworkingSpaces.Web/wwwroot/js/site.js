document.addEventListener("DOMContentLoaded", function() {
    const noResultsElement = document.getElementById("noResults");
    if (noResultsElement) {
        const searchQuery = noResultsElement.getAttribute("data-search-query") || "cat";
        fetch(`https://tenor.googleapis.com/v2/search?q=${searchQuery}&key=AIzaSyDpLaqpSphhTUnSnvHlY1gfneb7rDshjc4&limit=1`)
            .then(response => response.json())
            .then(data => {
                const gifUrl = data.results[0].media_formats.gif.url;
                const img = document.createElement("img");
                img.src = gifUrl;
                img.alt = "No results GIF";
                img.style.maxHeight = "75vh"; // Imposta l'altezza massima a 75vh
                noResultsElement.appendChild(img);
            })
            .catch(error => console.error('Error fetching GIF:', error));
    }
});