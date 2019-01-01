<template>
  <div id="app">
    <Home msg="Welcome to Your Vue.js + TypeScript App"/>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';
import Home from './components/Home.vue';

@Component({
  components: {
    Home,
  },
})
export default class App extends Vue {}
</script>
