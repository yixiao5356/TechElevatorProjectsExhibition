<template>
  <div class="card">
    <div class="card-body">
      <h1 class="card-title card-text-color">{{cardData.name}}</h1>
      
     <div v-html = "cardData.website" class="card-text-color">
     </div><br>
     

      <a class="btn btn-danger" href="#" @click.prevent="deleteEntry()">Delete</a>
    </div>
  </div>
</template>

<script>
import AdminService from "../services/AdminService.js";
export default {
  props: ["cardData"],
  components: {},

  data() {
    return {
      databaseItem: this.cardData,
      messageToUser: "",
      tempString: "",
    };
  },
  methods: {
    deleteEntry() {
      if (
        confirm(
          "Are you sure you want to delete this entry? This cannot be undone."
        )
      ) {
        AdminService.deleteEntry(this.databaseItem)
          .then(() => {
            location.reload();
            alert("Entry has been deleted.");
          })
          .catch((error) => {
            alert("Error in deleting." + error.response.data);
          });
      }
    },
  },
};
</script>

<style>
.card-text-color{
  color: var(--color-primary);
}
#adminDelete {
  display: flex;
  flex-direction: column;
}
.btn-danger:hover{
  background-color: darkred;
}
.card-subtitle{
  color:var(--font-color);
@media screen and (max-width: 425px) {
  .card{
    max-width: 400px;
  }
}
}

</style>