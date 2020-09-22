'use strict'

const Action = {
    BEGINS: "begins",
    WAKES: "wakes",
    SLEEPS: "sleeps",
}

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

// parse input
let formattedInput = input.map((line, index) => {
    let split1 = line.split(']');

    let moment = split1[0].substr(1);

    let date = moment.split(" ")[0];
    let splittedDate = date.split("-");

    let time = moment.split(" ")[1];
    let splittedTime = time.split(":");

    let event = split1[1].trim();
    let action;
    let guardId = null;
    if (event.startsWith("Guard")) {
        action = Action.BEGINS;
        guardId = parseInt(event.split('#')[1].split(" ")[0]);
    } else if (event.startsWith("wakes")) {
        action = Action.WAKES;
    } else if (event.startsWith("falls")) {
        action = Action.SLEEPS
    }

    let dateObj = new Date(splittedDate[0], splittedDate[1], splittedDate[2], splittedTime[0], splittedTime[1], 0, 0);

    return {
        datestr: date + " " + time,
        date: dateObj,
        guard: guardId,
        action: action,
    }
})

// first we must sort the dataset on date and time
let sortedInput = formattedInput.sort((a, b) => {
    if(a.datestr < b.datestr) { return -1; }
    if(a.datestr > b.datestr) { return 1; }
    return 0;

})

let guards = [];
let mostRecentGuardId;
sortedInput.forEach(event => {
    if (event.guard) {
       mostRecentGuardId = event.guard;
    }

    if (!guards[mostRecentGuardId]) {
        guards[mostRecentGuardId] = {
            id: mostRecentGuardId,
            totalSleep: 0,
            events: [],
            awake: true,
        }
    }

    let mostRecentGuard = guards[mostRecentGuardId];
    switch (event.action) {
        case Action.BEGINS: break;
        case Action.SLEEPS: {
            mostRecentGuard.events.push({
                start: event.date.getMinutes(),
                stop: null
            });
            mostRecentGuard.awake = false;
            break;
        }
        case Action.WAKES: {
            if (mostRecentGuard.awake) console.log("SOMETHING IS WRONG", mostRecentGuard.id)
            let mostRecentGuardLastEvent = mostRecentGuard.events[mostRecentGuard.events.length-1];
            mostRecentGuardLastEvent.stop = event.date.getMinutes()
            mostRecentGuard.totalSleep += (mostRecentGuardLastEvent.stop - mostRecentGuardLastEvent.start)
            mostRecentGuard.awake = true;
        }
    }
})

let sleepyGuard = guards.sort((a,b) => {
    return b.totalSleep - a.totalSleep;
})[0]


let minutes = []
sleepyGuard.events.forEach(event => {
    for (let i = event.start; i < event.stop; i++) {
        if (!minutes[i]) minutes[i] = { 
            count: 0, 
            time: i
        }
            
        minutes[i].count++;
    }
})


let bestMinute = minutes.sort((a,b) => {
    return b.count-a.count;
})[0].time

// console.log(sleepyGuard);
// console.log("id: ", sleepyGuard.id)
// console.log("best minute: ", bestMinute);
console.log("A: ",  sleepyGuard.id * bestMinute)

// A 76357

let sleepiest = []
for (let i = 0; i < 60; i++) {

    sleepiest[i] = {
        minute: i,
        guard: null,
        count: 0
    }

    guards.forEach(guard => {
        let count = 0;
        guard.events.forEach(event => {
            if (event.start <= i && event.stop > i) {
                count ++;
            }
        })
        if (count > sleepiest[i].count) {
            sleepiest[i].count = count;
            sleepiest[i].guard = guard.id;
        }
    })
}

let sleepyGuardB = sleepiest.sort((a,b) => {
    return b.count - a.count;
})[0]

let resultB = sleepyGuardB.minute * sleepyGuardB.guard
console.log("B: ", resultB)

// 41668