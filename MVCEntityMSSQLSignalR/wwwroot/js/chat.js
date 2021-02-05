(function () {
    var input = document.getElementById("message");

    input.addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("sendBtn").click();
        }
    });

    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

    hubConnection.on("Send", function (data) {
        let elem = document.createElement("p");
        elem.appendChild(document.createTextNode(data));
        elem.className("message-block");
        let firstElem = document.getElementById("chatroom").firstChild;
        document.getElementById("chatroom").insertBefore(elem, firstElem);
    });

    document.getElementById("sendBtn").addEventListener("click", function (e) {
        let message = document.getElementById("message").value;
        document.getElementById("message").value = "";
        hubConnection.invoke("Send", message);
    });

    document.getElementById("clearBtn").addEventListener("click", function (e) {
        document.getElementById("chatroom").remove();
        let chatRoomElem = document.createElement("div");
        chatRoomElem.id = "chatroom";
        document.getElementById("clearBtn").insertBefore(chatRoomElem);
    });

    hubConnection.start();
}());