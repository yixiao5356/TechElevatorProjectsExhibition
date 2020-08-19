<template>
  <div class="updateDiv">
    <h1>Update Topic:</h1>

    <form id="updateForm">
      <label for="categoryUpdate">Category:</label>
      <select v-model.number="databaseData.categoryId" id="categoryUpdate" class="mb-3">
        <option value="1">Pathway</option>
        <option value="2">Curriculum</option>
        <option value="4">Job</option>
        <option value="3">Motivational Quote</option>
        <option value="5">Companies</option>
      </select>
      <label for="nameUpdate">Name:</label>
      <input type="text" v-model="databaseData.name" id="nameUpdate" class="mb-3" />
      <label for="descriptionUpdate">Description:</label>
      <textarea v-model="databaseData.description" id="descriptionUpdate" class="mb-3"></textarea>
      <label for="websiteUpdate">Website (if applicable):</label>
      <textarea v-model="databaseData.website" id="websiteUpdate" class="mb-3"></textarea>
      <label for="weightUpdate">Weight:</label>
      <input type="number" v-model="databaseData.weight" id="weightUpdate" class="mb-3" />
      <label v-if="databaseData.categoryId === 5" for="location">Location:</label>
      <input
        v-if="databaseData.categoryId === 5"
        type="text"
        id="location"
        v-model="databaseData.location"
        class="mb-3"
      />
      <label v-if="databaseData.categoryId === 5" for="numberOfEmployees">Number of Employees</label>
      <input
        v-if="databaseData.categoryId === 5"
        type="text"
        id="numberOfEmployees"
        v-model="databaseData.numberOfEmployees"
        class="mb-3"
      />
      <label v-if="databaseData.categoryId === 5" for="numberOfGrads">Number of Graduates</label>
      <input
        v-if="databaseData.categoryId === 5"
        type="number"
        id="numberOfGrads"
        v-model="databaseData.numberOfGrads"
        class="mb-3"
      />
      <label v-if="databaseData.categoryId === 5" for="namesOfGrads">Names of Graduates</label>
      <input
        v-if="databaseData.categoryId === 5"
        type="text"
        id="namesOfGrads"
        v-model="databaseData.namesOfGrads"
        class="mb-3"
      />
      <label v-if="databaseData.categoryId === 5" for="glassdoorRating">Glassdoor Rating</label>
      <input
        v-if="databaseData.categoryId === 5"
        type="number"
        step="0.1"
        min="0"
        max="5"
        id="glassdoorRating"
        v-model="databaseData.rating"
        class="mb-3"
      />
      <div class="alert alert-danger mt-3" role="alert" v-if="errorMessages.length > 0">
        <ul>
          <li id="errorMessage" v-for="error in errorMessages" v-bind:key="error">{{error}}</li>
        </ul>
      </div>
      <input type="submit" class="btn btn-primary mb-3" v-on:click.prevent="sendToDatabase" />
    </form>
  </div>
</template>

<script>
import AdminService from "../services/AdminService.js";

export default {
  name: "UpdateDatabaseEntry",
  props: ["databaseEntry"],

  data() {
    return {
      databaseData: this.databaseEntry,
      errorMessages: [],
    };
  },
  methods: {
    sendToDatabase() {
      this.errorMessages = [];
      if (
        this.databaseData.name &&
        this.databaseData.description &&
        this.databaseData.weight >= 0
      ) {
        AdminService.updateEntry(this.databaseData)
          .then((response) => {
            if (response.status == 200) {
              alert("Entry Updated");
              this.$router.push({ name: "admin" });
            }
          })
          .catch((error) => {
            alert("Error in updating entry." + error.response.data);
          });
      } else {
        if (!this.databaseData.name) {
          this.errorMessages.push("Topic name required.");
        }
        if (!this.databaseData.description) {
          this.errorMessages.push("Topic description required.");
        }
        if (this.databaseData.weight < 0) {
          this.errorMessages.push("Topic weight must be greater than 0.");
        }
      }
    },
  },
};
</script>
    
<style>
.updateDiv {
  display: flex;
  flex-direction: column;
}

#updateForm {
  display: flex;
  flex-direction: column;
}
#errorMessage {
  color: black;
}
</style>