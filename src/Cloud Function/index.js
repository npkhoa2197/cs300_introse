const functions = require('firebase-functions');


// Create and Deploy Your First Cloud Functions
// https://firebase.google.com/docs/functions/write-firebase-functions

exports.deletePaidOrder = functions.database.ref('FinishedOrder/{orderID}').onWrite(event => {
	let promises = [];
	if (event.data.val().paid == true) {
		promises.push(event.data.ref.root.child('PaidOrder').push().set(event.data.val()));
		promises.push(event.data.ref.remove());
	}
	return Promise.all(promises);
	
});
exports.deleteFinishedOrder = functions.database.ref('Order/{orderID}').onWrite(event => {
	let promises = [];
	if (event.data.val().finished == true) {
		promises.push(event.data.ref.root.child('FinishedOrder').push().set(event.data.val()));
		promises.push(event.data.ref.remove());
	}
	return Promise.all(promises);
	
});