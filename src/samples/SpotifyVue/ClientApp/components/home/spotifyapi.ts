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
    artists: Artist[] = []
    auth: SpotifyAuthorization = { spotifyUsername: undefined, authorized: false, authorizationUrl: undefined }
    username: string = ""
    query: string = ""

    /**
     * Delegate for post message events from the popup user auth window
     * @param event The post message event
     */
    receiveMessage(event:MessageEvent)
    {
        this.auth.authorized = event.data;
    }

    mounted() {
        let searchButton = document.getElementById("searchButton");
        if (searchButton) searchButton.addEventListener("click", () => {
            fetch(`api/spotify/searchartists?query=${encodeURI(this.query)}`)
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
                        // setup to receive message from the popup
                        window.addEventListener("message", this.receiveMessage, false);
                        // open the popup
                        window.open(data.authorizationUrl, "winRingoAuth", "width=800,height=800")!.focus();
                    }
                })
                .catch(reason => console.error("error", reason))
        })
    }
}
