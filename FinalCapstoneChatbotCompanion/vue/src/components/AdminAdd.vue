<template>
  <div>
    <div id="adminAdd">
      <label for="category">Select which category to add to:</label>
      <select v-model.number="databaseItem.categoryId" class="mb-3">
        <option value="1">Pathway</option>
        <option value="2">Curriculum</option>
        <option value="4">Job</option>
        <option value="3">Motivational Quote</option>
        <option value="5">Companies</option>
      </select>
      <label for="Name">Name of topic:</label>
      <input type="text" id="Name" v-model="databaseItem.name" class="mb-3" />
      <label for="description">Description:</label>
      <textarea id="descriptions" v-model="databaseItem.description" class="mb-3"></textarea>
      <label for="website">Website (if applicable):</label>
      <textarea id="descriptions" v-model="databaseItem.website" class="mb-3"></textarea>
      <label v-if="databaseItem.categoryId === 5" for="numberOfEmployees">Number of Employees:</label>
      <input
        v-if="databaseItem.categoryId === 5"
        type="text"
        id="numberOfEmployees"
        v-model="databaseItem.numberOfEmployees"
        class="mb-3"
      />
      <label v-if="databaseItem.categoryId === 5" for="location">Location:</label>
      <input
        v-if="databaseItem.categoryId === 5"
        type="text"
        id="location"
        v-model="databaseItem.location"
        class="mb-3"
      />
      <label v-if="databaseItem.categoryId === 5" for="numberOfGrads">Number of Graduates:</label>
      <input
        v-if="databaseItem.categoryId === 5"
        type="number"
        id="numberOfGrads"
        v-model="databaseItem.numberOfGrads"
        class="mb-3"
      />
      <label v-if="databaseItem.categoryId === 5" for="namesOfGrads">Names of Graduates:</label>
      <input
        v-if="databaseItem.categoryId === 5"
        type="text"
        id="namesOfGrads"
        v-model="databaseItem.namesOfGrads"
        class="mb-3"
      />
      <label v-if="databaseItem.categoryId === 5" for="glassdoorRating">Glassdoor Rating:</label>
      <input
        v-if="databaseItem.categoryId === 5"
        type="number"
        step="0.1"
        min="0"
        max="5"
        id="glassdoorRating"
        v-model="databaseItem.rating"
        class="mb-3"
      />
      <div class="alert alert-danger mt-3" role="alert" v-if="errorMessages.length > 0">
        <ul>
          <li id="errorMessage" v-for="error in errorMessages" v-bind:key="error">{{error}}</li>
        </ul>
      </div>
      <button class="btn btn-primary mb-3" type="submit" v-on:click="sendToDatabase()">Send</button>
    </div>
  </div>
</template>

<script>
import AdminService from "../services/AdminService.js";
export default {
  data() {
    return {
      databaseItem: {
        name: "",
        description: "",
        categoryId: 0,
        weight: 0,
        location: "",
        numberOfEmployees: "",
        numberOfGrads: 0,
        namesOfGrads: "",
        rating: 0,
      },
      errorMessages: [],
    };
  },
  methods: {
    sendToDatabase() {
      this.errorMessages = [];
      if (
        this.databaseItem.name &&
        this.databaseItem.description &&
        this.databaseItem.categoryId > 0
      ) {
        AdminService.addToDatabase(this.databaseItem)
          .then((response) => {
            if (response.status == 200) {
              alert("Data successfully added");

              this.databaseItem = {
                name: "",
                description: "",
                categoryId: 0,
                weight: 0,
              };
            }
          })
          .catch((error) => {
            this.databaseItem = {
              name: "",
              description: "",
              categoryId: 0,
              weight: 0,
            };
            alert(
              "Error adding to database. Error received: " + error.response.data
            );
          });
      } else {
        if (!this.databaseItem.name) {
          this.errorMessages.push("Topic name required.");
        }
        if (!this.databaseItem.description) {
          this.errorMessages.push("Topic description required.");
        }
        if (this.databaseItem.categoryId == 0) {
          this.errorMessages.push("Topic Category required.");
        }
      }
    },
  },
};
</script>

<style>
#adminAdd {
  display: flex;
  flex-direction: column;
  color: var(--font-color);
}

#errorMessage {
  color: black;
}
</style>

