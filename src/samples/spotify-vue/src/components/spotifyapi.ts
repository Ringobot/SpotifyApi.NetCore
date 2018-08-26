import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as Models from './models'

interface Artist {
    id: string;
    name: string;
    url: string;
}

interface SpotifyAuthorization {
    userId?: string;
    authorized: boolean;
    authorizationUrl?: string;
}

@Component
export default class SpotifyApiComponent extends Vue {
    artists: Artist[] = []
    devices: Models.Device[] = []
    auth: SpotifyAuthorization = { userId: undefined, authorized: false, authorizationUrl: undefined }
    username: string = ""
    query: string = ""
    error: string = ""

    /**
     * Delegate for post message events from the popup user auth window
     * @param event The post message event
     */
    receiveMessage(event: MessageEvent) {
        this.auth.authorized = event.data;
    }

    playArtist(event:any) {
        fetch(`${process.env.VUE_API_BASE_URL}/api/spotify/playArtist?spotifyUri=${event.currentTarget.attributes["x-spotifyuri"].value}`,
            {
                method: "PUT"
            }
        )
            //.then(response => response.json() as Promise<Models.Device[]>)
            //.then(data => {
            //    this.devices = data;
            //})
            .catch(reason => {
                console.error("error", reason)
                this.error = reason;
            })

    }


    mounted() {
        // refresh devices
        document.getElementById("refreshDevicesButton")!.addEventListener("click", () => {
            fetch(`${process.env.VUE_API_BASE_URL}/api/spotify/devices`)
                .then(response => response.json() as Promise<Models.Device[]>)
                .then(data => {
                    this.devices = data;
                })
                .catch(reason => {
                    console.error("error", reason)
                    this.error = reason;
                })

        })

        // search for artist
        document.getElementById("searchButton")!.addEventListener("click", () => {
            fetch(`${process.env.VUE_API_BASE_URL}/api/spotify/searchartists?query=${encodeURI(this.query)}`)
                .then(response => response.json() as Promise<Artist[]>)
                .then(data => {
                    this.artists = data;
                })
                .catch(reason => {
                    console.error("error", reason)
                    this.error = reason;
                })

        })

        // authorize user
        document.getElementById("authButton")!.addEventListener("click", (e: Event) => {
            fetch(`${process.env.VUE_APP_API_BASE_URL}/api/spotify/authorize`,
                {
                    method: "POST"
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
                .catch(reason => {
                    console.error("error", reason)
                    this.error = reason;
                })
        })
    }
}
