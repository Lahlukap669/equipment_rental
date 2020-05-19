<template>
<div style="padding: 30px; width: 60%; margin-left: 20%;">
    <div v-for="item in items" class="card" style="width: 28%; height: 22rem; float: left; margin:2%; background-color: #dddddd;">
        <div class="card-body">
            <h5 class="card-title">{{ item.id }}</h5>
            <h6 class="card-subtitle mb-2">Stanje: {{ item.stanje }}</h6>
            <h6 class="card-subtitle mb-2 text-muted">Oprema: {{ item.stanje }}</h6>
            <h6 class="card-subtitle mb-2 text-muted">Nadzornik: {{ item.nadzornik }}</h6>
            <h6 class="card-subtitle mb-2 text-muted">Plačnik: {{ item.placnik }}</h6>
            <h7 class="card-subtitle mb-2 text-muted"><i>Datum-od: {{ item.datum_od }}</i></h7></br>
            <h7 class="card-subtitle mb-2 text-muted"><i>Datum-do: {{ item.datum_do }}</i></h7></br></br>
            <p class="card-text">Poročilo: {{ item.opis }}</p>
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
        }
        
    }
}
</script>