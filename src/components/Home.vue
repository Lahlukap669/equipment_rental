<template>
<div style="padding: 30px">
    <button class="button btn-danger" @click="logout()" >Logout</button>
    <div v-for="item in items" class="card" style="width: 18rem; float: left">
        <div class="card-body">
            <h5 class="card-title">{{ item.opis }}</h5>
            <h6 class="card-subtitle mb-2 text-muted">Card subtitle</h6>
            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
            <a href="#" class="card-link">Card link</a>
            <a href="#" class="card-link">Another link</a>
        </div>
    </div>
</div>
</template>

<script>
export default {
    data: () => ({
        items: []
    }),
    created: function() {
      this.fetchIzposoje();  
    },
    methods: {
        fetchIzposoje() {
            fetch('https://equipment-rental.herokuapp.com/vizposoje', {
                method: "POST",
                headers: { "Content-Type": "application/json" }
            }).then(res => res.json()).then(result => {
                console.log(result);
                this.items = result;
            })
        },
        logout() {
            localStorage.removeItem('id');
            this.$router.push('/login');
        }
    }
}
</script>