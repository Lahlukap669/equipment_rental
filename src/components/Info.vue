<template>
<div style="padding: 30px; width: 60%; margin-left: 20%; height: 40%;">
    <div class="card text-white bg-dark mb-3" style="max-width: 100%;">
        <div class="card-header"><span class="badge badge-info">Info</span></div>
        <div class="card-body">
            <h5 class="card-title"><u>User info:</u></h5>
            <p class="card-text">Ime: {{ data.ime }}</p>
            <p class="card-text">Priimek: {{ data.priimek }}</p>
            <p class="card-text">Email: {{ data.email }}</p>
            <p class="card-text">Telefonska številka: {{ data.tel }}</p>
        </div>
    </div>
</div>
</template>

<script>
export default {
    data: () => ({
        req: {id:localStorage.getItem('id')},
        data: Object
    }),
    created: function() {
      this.fetchInfo();  
    },
    methods: {
        fetchInfo() {
            fetch('https://equipment-rental.herokuapp.com/userinfo', {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(this.req)
            }).then(res => res.json()).then(result => {
                console.log(result);
                this.data = result;
            })
        }       
    }
}
</script>