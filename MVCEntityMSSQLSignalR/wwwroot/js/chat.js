(function () {
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    hubConnection.on("Send", function (data) {
        let elem = document.createElement("p");
        elem.appendChild(document.createTextNode(data));
        let firstElem = document.getElementById("chatroom").firstChild;
        document.getElementById("chatroom").insertBefore(elem, firstElem);
    });

    document.getElementById("sendBtn").addEventListener("click", function (e) {
        let message = document.getElementById("message").value;
        document.getElementById("message").value = "";

        if (message.includes('\\') && !message.includes('\\info')) {
            clear();
            hubConnection.invoke("Send", message);

            if (!message.match('\\get'))
                hubConnection.invoke("Send", "\\get");
        }
        else {
            hubConnection.invoke("Send", message);
        }
    });

    document.getElementById("clearBtn").addEventListener("click", function (e) {
        clear();
    });

    document.getElementById("message").addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("sendBtn").click();
        }
    });

    hubConnection.start();

    function clear() {
        document.getElementById("chatroom").remove();
        let chatRoomElem = document.createElement("div");
        chatRoomElem.id = "chatroom";
        let clearBtn = document.getElementById("clearBtn");
        document.getElementById("chatpanel").insertBefore(chatRoomElem, clearBtn);
    }
}());