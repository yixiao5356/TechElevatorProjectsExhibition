<template>
  <div>
    <h1 class="selectUpdate">Filters:</h1>
    <div id="selectNav">
      <span class="filterLabel">
        <label for="categoryFilter" class="labelForFilter">Category:</label>
        <select v-model.number="filter.categoryId" id="categoryFilter">
          <option value="0">All</option>
          <option value="1">Pathway</option>
          <option value="2">Curriculum</option>
          <option value="4">Job</option>
          <option value="3">Motivational Quote</option>
          <option value="5">Companies</option>
        </select>
      </span>
      <span class="filterLabel">
        <label for="nameFilter" class="labelForFilter">Name:</label>
        <input type="text" v-model="filter.name" id="nameFilter" />
      </span>
      <span class="filterLabel">
        <label for="descriptionFilter" class="labelForFilter">Description:</label>
        <input type="text" v-model="filter.description" id="descriptionFilter" />
      </span>
      <span class="filterLabel">
        <label for="weightFilterBehavior" class="labelForFilter">Weight Filter Behavior:</label>
        <select v-model="filter.weightFilterBehavior" id="weightFilterBehavior">
          <option>Equal To</option>
          <option>Greater than or Equal To</option>
        </select>
      </span>
      <span class="filterLabel">
        <label for="weightFilter" class="labelForFilter">Weight:</label>
        <input type="number" min="0" v-model.number="filter.weight" id="weightfilter" />
      </span>
    </div>
    <div id="divCard">
      <DatabaseCard
        v-for="databaseItem in filteredList"
        v-bind:key="databaseItem.description"
        v-bind:cardData="databaseItem"
      />
    </div>
  </div>
</template>

<script>
import AdminService from "../services/AdminService.js";
import DatabaseCard from "../components/DatabaseCard.vue";
export default {
  components: {
    DatabaseCard,
  },
  data() {
    return {
      databaseItems: [],
      filter: {
        categoryId: 0,
        name: "",
        description: "",
        website: "",
        weight: 0,
        weightFilterBehavior: "Greater than or Equal To",
      },
    };
  },
  computed: {
    filteredList() {
      let results = this.databaseItems;
      if (this.filter.categoryId > 0) {
        const categoryIdFilter = this.filter.categoryId;
        results = results.filter(
          (item) => item.categoryId === categoryIdFilter
        );
      }
      if (this.filter.name) {
        const nameFilter = this.filter.name.toLowerCase();
        results = results.filter((item) =>
          item.name.toLowerCase().includes(nameFilter)
        );
      }
      if (this.filter.description) {
        const descriptionFilter = this.filter.description.toLowerCase();
        results = results.filter((item) =>
          item.description.toLowerCase().includes(descriptionFilter)
        );
      }
      if (
        this.filter.weight >= 0 &&
        this.filter.weightFilterBehavior == "Equal To"
      ) {
        const weightFilter = this.filter.weight;
        results = results.filter((item) => item.weight === weightFilter);
      }
      if (
        this.filter.weight >= 0 &&
        this.filter.weightFilterBehavior == "Greater than or Equal To"
      ) {
        const weightFilter = this.filter.weight;
        results = results.filter((item) => item.weight >= weightFilter);
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
    AdminService.getAllDatabaseEntries()
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

#selectNav {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around;
}

.filterLabel{
  display: flex;
  margin: 5px;
}

.labelForFilter {
  margin-right: 5px;
}

</style>
