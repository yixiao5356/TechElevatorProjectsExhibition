<template>
  <div>
    <h1 class="selectUpdate">Filters:</h1>
    <div id="selectNav">
      <span class="filterLabel">
        <label for="nameFilter" class="labelForFilter">Name:</label>
        <input type="text" v-model="filter.name" id="nameFilter" />
      </span>
    </div>
    <div id="divCard">
      <RequestCard
        v-for="databaseItem in filteredList"
        v-bind:key="databaseItem.name"
        v-bind:cardData="databaseItem"
      />
    </div>
  </div>
</template>

<script>
import AdminService from "../services/AdminService.js";
import RequestCard from "../components/RequestCard.vue";
export default {
  components: {
    RequestCard,
  },
  data() {
    return {
      databaseItems: [],
      filter: {
        name: "",
      },
    };
  },
  computed: {
    filteredList() {
      let results = this.databaseItems;
      if (this.filter.name) {
        const nameFilter = this.filter.name;
        results = results.filter((item) =>
          item.name.toLowerCase().includes(nameFilter)
        );
      }
      return results;
    },
  },
  methods: {
    MoveToDatabaseEntryDetails() {
      this.$router.push();
    },
  },
  created() {
    AdminService.getAllUserRequests()
      .then((response) => {
        this.databaseItems = response.data;
      })
      .catch((error) => {
        this.messageToUser =
          "Error adding to database. Error received: " + error.response.data;
      });
  },
};
</script>

<style>
.selectUpdate {
  color: var(--font-color);
}

.card {
  background-color: rgb(175, 174, 174);
}
</style>
