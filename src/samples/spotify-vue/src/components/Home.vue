<template>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1>Hello Spotify API</h1>
                <p>
                    Samples of usage of <code>SpotifyApi.NetCore</code>. Source code is here:
                    <a href="https://github.com/Ringobot/SpotifyApi.NetCore/tree/master/src/samples">https://github.com/Ringobot/SpotifyApi.NetCore/tree/master/src/samples</a>
                </p>

                <h2>Search for an Artist</h2>

                <p>Who's your favourite Artist?</p>

                <form class="form-inline" onclick="return false">
                    <div class="form-group">
                        <input id="query" v-model="query" placeholder="e.g. Radiohead" class="form-control">
                        <button v-on:click="searchArtists" class="btn btn-success">Search</button>
                    </div>
                </form>

                <div style="margin-top:20px;" />

                <table v-if="artists.length" class="table table-condensed table-hover">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in artists">
                            <td>{{ item.name }}</td>
                            <td>
                                <button :x-spotifyuri="item.uri" v-on:click="playArtist($event)" class="btn btn-primary" :disabled="!auth.authorized || devices.length === 0">Play</button>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <h2>User Authorization</h2>

                <p>
                    Spotify requires a user's authorization to control their device(s). Click the <strong>Authorize</strong>
                    button to initiate an <a href="https://developer.spotify.com/documentation/general/guides/authorization-guide/#authorization-code-flow">Authorization Code Flow</a>
                    as described in the Spotify developer authorization guide.
                </p>

                <div class="form-group">
                    <button v-on:click="authorizeUser" class="btn btn-danger">Authorize</button>
                </div>

                <p v-if="auth.authorized">
                    User Id <code>{{ auth.userId }}</code> is authorized ✅
                </p>

                <h2>User's devices</h2>
                <p>
                    The <a href="https://developer.spotify.com/documentation/web-api/reference/player/">Player</a> API
                    includes an endpoint to get a user's available devices. Only Devices that have Spotify open and connected
                    will be shown here. On mobile devices you may need to play a track before the device will be shown here.
                </p>

                <div class="form-group">
                    <button v-on:click="getDevices" :disabled="!auth.authorized" class="btn btn-success">Get Devices</button>
                </div>

                <p v-if="gotDevices && devices.length === 0" style="padding:15px;background-color: #fcf8e3;">No available devices found</p>

                <table v-if="devices.length" class="table table-condensed">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Type</th>
                            <th>Active</th>
                            <th>Volume</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="item in devices">
                            <td>{{ item.name }}</td>
                            <td>{{ item.type }}</td>
                            <td>{{ item.is_active }}</td>
                            <td>{{ item.volume_percent }}</td>
                        </tr>
                    </tbody>
                </table>

                <p v-if="error" class="bg-danger">Error: {{ error }}</p>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component
    export default class Home extends Vue {
        @Prop() private msg!: string;
    }
</script>

<script src="./spotifyapi.ts"></script>