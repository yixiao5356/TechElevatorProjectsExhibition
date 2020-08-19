<template>
  <div class="card">
    <div class="card-body">
      <h3 class="card-title card-text card-text-color">{{cardData.name}}</h3>
      <h4 class="card-subtitle mb-2 card-text card-text-color">Category: {{cardData.categoryName}}</h4>
      <h5 class="card-subtitle mb-2 card-text card-text-color">Weight: {{cardData.weight}}</h5>
      <p class="card-text card-text-color">
        Description:
        <br />
        {{cardData.description}}
      </p> 
      <p class="card-text card-text-color" v-if="cardData.categoryId === 5">
        Company Data:
        <br />
        Location: {{cardData.location}}
        <br />
        Number of Employees: {{cardData.numberOfEmployees}}
        <br />
        Number of Graduates: {{cardData.numberOfGrads}}
        <br />
        Names of Graduates: {{cardData.namesOfGrads}}
        <br />
        Glassdoor Rating: {{cardData.rating}}
      </p>
      
     <p class="card-text-color">{{cardData.website}}
     </p>
     
      <router-link
        class="btn btn-primary card-text-color"
        tag="button"
        :to="{name: 'EditDatabaseDetails', params: {categoryName: cardData.categoryName,categoryId: cardData.categoryId,databaseEntry: cardData}}"
      >Update</router-link>
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
}
@media screen and (max-width: 425px) {
  .card{
    max-width: 90vw;
  }
}


</style>