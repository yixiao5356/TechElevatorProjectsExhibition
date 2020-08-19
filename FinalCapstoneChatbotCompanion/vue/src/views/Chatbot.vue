<template>
  <div class="chatbot">
    <div class="header">
      <h1>Liftman</h1>
    </div>
    <div class="chatBox" v-chat-scroll>
      <div v-for="item of messageLog" v-bind:key="item.sender">
        <div id="messageContainer">
          <!--<p v-bind:class="{botMessageIdentifier: item.sender === 'Bot', userMessageIdentifier: item.sender === userName}">{{item.sender}}</p>-->
          <br />
          <div
            class="alignprofile"
            v-bind:class="{botMessage: item.sender === 'Bot', userMessage: item.sender === userName}"
          >
            <img
              v-if="item.sender === 'Bot'"
              class="profile"
              src="../../public/techelevatorpro.png"
              alt="profile"
            />
            <pre
              v-bind:class="{botMessageBackground: item.sender === 'Bot', userMessageBackground: item.sender === userName}"
              class="message"
              v-html="item.text"
            ></pre>
          </div>
        </div>
      </div>
    </div>
    <div
      v-if="messageLog[messageLog.length - 1].text.includes('Is this what you were looking for (please click yes or no)?') && messageLog[messageLog.length - 1].sender == 'Bot'"
    >
      <div class="btn-group yes-no-button input">
        <input
          class="btn btn-lg btn-primary"
          type="button"
          value="Yes"
          v-on:click="sendButtonMessage"
        />
        <input
          class="btn btn-lg btn-primary"
          type="button"
          value="No"
          v-on:click="sendButtonMessage"
        />
      </div>
    </div>
    <div v-else class="input-group input text-input">
      <div class="btn-group yes-no-button mb-1">
        <input class="btn btn-sm btn-primary" type="button" v-for="button in quickSelectButtons" v-bind:key="button.name" v-bind:value="button.name" v-on:click="sendButtonMessage" v-bind:disabled="messageLog.length < 2" />
      </div>
      <div class="input-group">
        <input
          type="text"
          class="form-control textbox"
          autofocus
          placeholder
          v-model="textToInput"
          aria-label="Input to type text to talk with bot"
          v-on:keyup.enter="sendMessage()"
        />
        <div class="input-group-append">
          <button
            class="btn btn-primary"
            id="submitbutton"
            type="submit"
            v-on:click="sendMessage()"
          >Send</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import service from "../services/AuthService.js";
import QuickSelectService from "../services/QuickSelectService.js"

export default {
  name: "chatbot",
  data() {
    return {
      messageLog: [
        {
          sender: "Bot",
          text: "Hello, what is your name?",
        },
      ],
      userName: "",
      textToInput: "",
      quickSelectButtons: [],
    };
  },
  created() {
    QuickSelectService.getQuickSelectButtonTopics()
    .then((response) => {
      this.quickSelectButtons = response.data;
    })
    .catch(() => {
      console.log("error getting quick select buttons");
    })
  },
  methods: {
    sendMessage() {
      if (this.userName == "" && this.textToInput != "") {
        this.userName = this.textToInput;
        let sendup = {
          text: "asdewwega " + this.userName,
        };
        this.response(sendup);
        let objectToPush = {
          sender: "",
          text: "",
        };
        objectToPush = {
          sender: this.userName,
          text: this.textToInput,
        };
        this.messageLog.push(objectToPush);
        this.textToInput = "";
        return;
      }
      let objectToPush = {
        sender: "",
        text: "",
      };
      objectToPush.sender = this.userName;
      objectToPush.text = this.textToInput;
      let setup = {
        text: this.textToInput,
      };
      if (this.textToInput != "") {
      this.messageLog.push(objectToPush);
      this.textToInput = "";

      this.response(setup);
    }
    },
    response(text) {
      service.response(text).then((item) => {
        this.messageLog.push(item.data);
      });
    },
    sendButtonMessage(event) {
      let objectToPush = {
        sender: "",
        text: "",
      };
      objectToPush.sender = this.userName;
      objectToPush.text = event.target.value;
      let setup = {
        text: event.target.value,
      };
      this.messageLog.push(objectToPush);
      this.textToInput = "";

      this.response(setup);
    },
  },
};
</script>

<style>

.profile {
  width: 30px;
  height: 30px;
  margin: 0px 0px 0px 5px;
}

.textbox {
  overflow-wrap: break-word;
  word-break: break-all;
}

.chatbot {
  display: grid;
  grid-template-areas:
    "header"
    "interactbox"
    "input";
  grid-template-rows: auto 70vh 10vh;
  background-color: var(--color-primary);
  color: var(--font-color);
}
.yes-no-button {
  display: flex;
  justify-content: space-around;
}

.footer {
  align-items: center;
}

.header {
  grid-area: "header";
  align-items: center;
}

.chatBox {
  grid-area: "interactbox";
  border: var(--color-accent) solid 5px;
  border-radius: 10px;
  overflow: auto;
  overflow-anchor: inherit;
  background-color: var(--color-primary);
  margin-bottom: 5px;
}

.input {
  grid-area: "input";
}

.messageContainer {
  flex-wrap: nowrap;
}

.message {
  border: solid;
  border-radius: 10px;
  margin: 0px 10px 10px 10px;
  padding: 5px;
  overflow-wrap: break-word;
  white-space: pre-line;
  border-color: var(--color-accent);
  color: var(--bot-font-color);
}
.alignprofile {
  display: flex;
}
.botMessageBackground {
  background: var(--color-botmessage);
  color: var(--bot-font-color);
}
.botMessage {
  max-width: 50%;
  align-self: flex-start;
}
.userMessageBackground {
  background-color: var(--bot-font-color);
  color: var(--color-botmessage);
}
.userMessage {
  align-self: flex-end;
  max-width: 50%;
  margin-bottom: 0px;
}

#messageContainer {
  display: flex;
  flex-direction: column;
}
.botMessageIdentifier {
  margin-left: 10px;
  margin-bottom: 0px;
}

.userMessageIdentifier {
  display: none;
  margin-right: 10px;
  margin-bottom: 0px;
  align-self: flex-end;
}

.text-input {
  display: flex;
  flex-direction: column;
  flex-wrap: nowrap;
}

@media screen and (max-width: 425px) {
  .userMessage {
    max-width: 85%;
  }
  .botMessage {
    max-width: 85%;
  }
}
</style>