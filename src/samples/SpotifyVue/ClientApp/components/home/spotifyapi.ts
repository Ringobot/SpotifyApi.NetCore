import Vue from 'vue';
import { Component } from 'vue-property-decorator';

interface Artist {
    id: string;
    name: string;
}

interface SpotifyAuthorization {
    spotifyUsername?: string;
    authorized: boolean;
    authorizationUrl?: string;
}

@Component
export default class SpotifyApiComponent extends Vue {
    artists: Artist[] = [];
    auth: SpotifyAuthorization = { spotifyUsername: undefined, authorized: false, authorizationUrl: undefined };
    username: string = ""

    mounted() {
        let searchButton = document.getElementById("searchButton");
        if (searchButton) searchButton.addEventListener("click", (e: Event) => {
            let queryInput = <HTMLInputElement>document.getElementById("query");
            let query: string = (queryInput) ? queryInput.value : ""
            fetch(`api/spotify/searchartists?query=${encodeURI(query)}`)
                .then(response => response.json() as Promise<Artist[]>)
                .then(data => {
                    this.artists = data;
                })
        })

        let authButton = document.getElementById("authButton");
        if (authButton) authButton.addEventListener("click", (e: Event) => {
            //let username = <HTMLInputElement>document.getElementById("username");

            fetch('api/spotify/authorize',
                {
                    method: "POST",
                    body: JSON.stringify({ SpotifyUsername: this.username }),
                    headers: {
                        "Content-Type": "application/json; charset=utf-8",
                    }
                })
                .then(response => response.json() as Promise<SpotifyAuthorization>)
                .then(data => {
                    this.auth = data;
                    if (data.authorizationUrl) {
                        window.open(data.authorizationUrl);
                    }
                })
                .catch(reason => console.error("error", reason))
        })
    }
}
