function spawnNotification(body, title, OnClickLink) {
		var options = {
				body: body
		};
		var n = new Notification(title, options)
		n.onclick = function (event) {
				event.preventDefault();
				window.location.href = OnClickLink;
		};
}

function subscribe() {
		var source = new EventSource('http://localhost:65492/api/Notifications/subscribe/');
		source.onerror = function () {
				console.log(arguments);
		};
		source.onmessage = function (e) {
				var data = JSON.parse(e.data);
				console.log(data);
				spawnNotification(data.Message, data.Title), data.OnClickLink;
		};
}
if (Notification.permission === "granted") {
		subscribe();
}
else if (Notification.permission !== "denied") {
		Notification.requestPermission().then(function (permission) {
				if (permission === "granted") {
						subscribe()
				}
		});
}
