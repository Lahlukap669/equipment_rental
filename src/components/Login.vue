<template>
<form style="margin-left: 20%; width: 60%">
  <div class="form-group">
    <label for="exampleInputEmail1">Email address</label>
    <input type="text" v-model="email" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">
    <small id="emailHelp" class="form-text text-muted">We'll never share your email with anyone else.</small>
  </div>
  <div class="form-group">
    <label for="exampleInputPassword1">Password</label>
    <input type="password" v-model="geslo" class="form-control" id="exampleInputPassword1">
  </div>
  <button @click="login()" class="btn btn-primary">Submit</button>
</form>
</template>

<script>
export default {
    data: () => ({
        email: '',
        geslo: ''
    }),
    methods: {
        login() {
            window.event.preventDefault();
            fetch('https://equipment-rental.herokuapp.com/login', {
                method: "POST",
                body: JSON.stringify({ email: this.email, geslo: this.geslo }),
                headers: { "Content-Type": "application/json" }
            }).then(res => res.json()).then(result => {
                if(result.bool!=0 || result.bool!=false){
                    localStorage.setItem('id', result.bool);
                    this.$router.push('/');
                }
                else{
                    alert("motiš se fant");
                }
                
            })
        }
    }
}
</script>