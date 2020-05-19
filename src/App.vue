<template>
  <div style="background-color: #5577ff; height: 100%;">
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <a class="navbar-brand" href="/">EQRent</a>
      <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>

      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto">
          <li class="nav-item active">
            <a class="nav-link" href="/info"> Userinfo <span class="sr-only">(current)</span></a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="https://github.com/Lahlukap669/equipment_rental">Github</a>
          </li>
        </ul>
        <button class="btn my-2 my-sm-2 btn-danger" @click="logout()">Logout</button>
      </div>
    </nav>
    <router-view/>
  </div>
</template>

<script>
export default {
  name: 'app',
  data () {
    return {
      msg: 'Welcome to Your Vue.js App'
    }
  },
  methods: {
    logout() {
            try{
            localStorage.removeItem('id');
            }catch(err){console.log(err);}
            this.$router.push('/login');
        }
  }
}
</script>