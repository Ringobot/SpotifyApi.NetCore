import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as Models from './models';

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
    public artists: Artist[] = [];
    public gotDevices: boolean = false;
    public devices: Models.Device[] = [];
    public auth: SpotifyAuthorization = { userId: undefined, authorized: false, authorizationUrl: undefined };
    public username: string = '';
    public query: string = '';
    public error: string = '';

    //public mounted() { }

    public getDevices() {
        fetch(`${process.env.VUE_APP_API_BASE_URL}/api/spotify/devices`, { credentials: 'include' })
            .then(response => response.json() as Promise<Models.Device[]>)
            .then(data => {
                this.devices = data;
                this.gotDevices = true;
            })
            .catch(reason => {
                console.error('error', reason);
                this.error = reason;
            });
    }

    public playArtist(event: any) {
        fetch(
            `${process.env.VUE_APP_API_BASE_URL}/api/spotify/playArtist`
            + `?spotifyUri=${event.currentTarget.attributes['x-spotifyuri'].value}`,
            {
                method: 'PUT',
                credentials: 'include'
            })
            .catch(reason => {
                console.error('error', reason);
                this.error = reason;
            });
    }

    public searchArtists() {
        fetch(
            `${process.env.VUE_APP_API_BASE_URL}/api/spotify/searchartists`
            + `?query=${encodeURI(this.query)}`, { credentials: 'include' })
            .then(response => response.json() as Promise<Artist[]>)
            .then(data => {
                this.artists = data;
            })
            .catch(reason => {
                console.error('error', reason);
                this.error = reason;
            });
    }

    public authorizeUser() {
        fetch(
            `${process.env.VUE_APP_API_BASE_URL}/api/spotify/authorize`,
            {
                method: 'POST',
                credentials: 'include'
            })
            .then(response => response.json() as Promise<SpotifyAuthorization>)
            .then(data => {
                this.auth = data;
                if (data.authorizationUrl) {
                    // setup to receive message from the popup
                    window.addEventListener('message', this.receiveMessage, false);
                    // open the popup
                    window.open(data.authorizationUrl, 'winRingoAuth', 'width=800,height=800')!.focus();
                }
            })
            .catch(reason => {
                console.error('error', reason);
                this.error = reason;
            });
    }

    /**
     * Delegate for post message events from the popup user auth window
     * @param event The post message event
     */
    private receiveMessage(event: MessageEvent) {
        this.auth.authorized = event.data;
    }
}
