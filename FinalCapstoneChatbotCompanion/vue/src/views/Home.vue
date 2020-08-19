<template>
  <div class="home-page">
    <div class="header">
      <h2 class="homeTitle">Welcome to Liftman, your elevator TEchnician</h2>
      <h2 class="homeTitle">I will lift you where you need to go!</h2>
    </div>
    <div class="home">
      <img src="../../public/TE Robot.png" alt="Liftman" class="robot" />
      <router-link tag="button" class="chat btn btn-primary btn-lg" v-bind:to="{name: 'chat'}">Get a lift!</router-link>
    </div>
    <div class="otherResources">
      <br />
      <p id="resources"> Other Resources: </p>
      <a
      class="resourceText"
        add
        target="_blank"
        href="https://book.techelevator.com/"
      >Check out the Tech Elevator Student Book</a>
      <a
      class="resourceText"
        add
        target="_blank"
        href="https://www.techelevator.com/faqs"
      >Check out frequently asked questions</a>
    </div>
  </div>
</template>

<script>
import Chatbot from "../views/Chatbot.vue";

export default {
  name: "home",
  components: Chatbot,
  data() {
    return {};
  },
};
</script>

<style>
.home-page {
  display: grid;
  grid-template-areas:  "header"
                        "center"
                        "bottom";
}

.home {
  grid-area: "center";
  display: flex;
  flex-direction: column;
  align-items: center;
  background: var(--color-primary);
  color: var(--font-color);
}
.homeTitle {
  text-align: center;
  color: var(--font-color);
}

.robot{
  max-width: 100%;
  height: auto;
}

.header :scope {
  grid-area: "header";
  text-align: center;
  justify-content: center;
  align-items: center;
  margin: 50px 0px 10px 0px;
}

.otherResources {
  grid-area: "bottom";
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  text-align: center;
  font-size: 20px;
}

.faq {
  grid-area: right;
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  text-decoration: underline;
  font-size: 20px;
}


.resourceText{
  color: var(--font-color);
  text-align: center;
  text-decoration: underline;
}

#resources{
  color: var(--font-color);
  text-shadow: 1px 1px var(--color-primary);
  font-size: 24px;
  font-weight: bold;
  text-decoration: none;
}
</style>