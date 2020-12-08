function subscribeUpdates(id) {
    const connection = new signalR.HubConnectionBuilder().withUrl(
        "/subcategory?s_id=" + id
    ).build();

    connection.on("HW", function (message) {
        alert(message);
    });

    connection.start().then(function () {
        alert("Start");
    }).catch(function (err) {
        return console.error(err.toString());
    });
}

