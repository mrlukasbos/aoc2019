'use strict'

console.log("A: ", solve(71436, 466))
console.log("B: ", solve(7143600, 466))

function solve(marbleCount, playerCount) {
    let newValue = 0;
    let marbles = [];
    marbles.push({val: 0})
    let currentMarble = marbles[0]
    currentMarble.next = currentMarble
    currentMarble.prev = currentMarble
    let currentPlayerId = 0;
    let playerScores = {};
    while (newValue <= marbleCount) {
        newValue++;
        currentPlayerId = newValue % playerCount

        if (newValue % 23 === 0) {
            playerScores[currentPlayerId] = (playerScores[currentPlayerId] || 0) + newValue

            let marbleToRemove = currentMarble;
            for (let i = 0; i < 7; i++) {
                marbleToRemove = marbleToRemove.prev;
            }
            playerScores[currentPlayerId] = (playerScores[currentPlayerId] || 0) + marbleToRemove.val
            currentMarble = marbleToRemove.next;

            // remove links to the marbleToRemove
            marbleToRemove.prev.next = marbleToRemove.next;
            marbleToRemove.next.prev = marbleToRemove.prev;

        } else {
            let newMarble = {
                val: newValue,
                prev: currentMarble.next,
                next: currentMarble.next.next,
            }
            newMarble.prev.next = newMarble;
            newMarble.next.prev = newMarble;
            marbles.push(newMarble)
            currentMarble = newMarble
        }
    }

    let bestScore = Object.keys(playerScores).map((key) => {
        return playerScores[key];
    }).sort((a,b) => b - a)[0];

    return bestScore;
}
