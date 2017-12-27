// Create and Deploy Your First Cloud Functions
// https://firebase.google.com/docs/functions/write-firebase-functions
const functions = require('firebase-functions');

exports.calAveRate = functions.database.ref('Meal/{mealID}/Rating/{ratingID}').onWrite(event => {
	let promises = [];
	event.data.ref.parent.parent.child('AveRating').once('value').then(snap => {
      oldAve = parseFloat(snap.val().Rate);
      noOfRating = parseFloat(snap.val().NoOfRating);   
      newRate = parseFloat(event.data.val());
      newAve = (oldAve * noOfRating + newRate)/(noOfRating+1);
      noOfRating +=1;

      promises.push(event.data.ref.parent.parent.child('AveRating/Rate').set(newAve));
	  promises.push(event.data.ref.parent.parent.child('AveRating/NoOfRating').set(noOfRating));
    });
	
	
	return Promise.all(promises);
	
});
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