const functions = require('firebase-functions');


// Create and Deploy Your First Cloud Functions
// https://firebase.google.com/docs/functions/write-firebase-functions

exports.deletePaidOrder = functions.database.ref('Order/{orderID}').onWrite(event => {
	if (event.data.val().Paid == true) {
		var newOrder = event.data.ref.root.child('PaidOrder').push();
		newOrder.set(event.data.val());
		return event.data.ref.remove();
	}
	
	return event.data.ref.set(event.data.val());
});
