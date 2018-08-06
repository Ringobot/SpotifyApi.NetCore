import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Artist {
    id: string;
    name: string;
}

@Component
export default class SpotifyApiComponent extends Vue {
    artists: Artist[] = [];

    mounted() {
        let searchButton = document.getElementById("searchButton");
        if (searchButton) searchButton.addEventListener("click", (e: Event) => {
            let queryInput = <HTMLInputElement>document.getElementById("query");
            let query: string = (queryInput) ? queryInput.value : ""
            fetch(`api/spotify/searchartists?query=${encodeURI(query)}`)
                .then(response => response.json() as Promise<Artist[]>)
                .then(data => {
                    this.artists = data;
                });
        })
    }
}
