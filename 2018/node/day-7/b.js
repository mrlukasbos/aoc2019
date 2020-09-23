'use strict'

const fs = require('fs');
let input = fs.readFileSync('input.txt','utf8').split("\n")

let instructions = input.map(entry => {
    let split = entry.toUpperCase().split("STEP ");
    return {
        first: split[1].charAt(0),
        next: split[2].charAt(0)
    }
})


let steps = instructions
    .map(instruction => {
        return instruction.first;
    })
    .map(step => {
        let followUps = instructions.filter(instruction => {
            return instruction.first == step
        }).map(instruction => {
            return instruction.next;
        });

        let requirements = instructions.filter(instruction => {
            return instruction.next == step
        }).map(instruction => {
            return instruction.first;
        })
        return {
            step,
            followUps,
            requirements, 
            finished: 0,
        }
})

instructions.forEach(ins => {
    if (!steps.some(step => step.step === ins.next)) {

        let requirements = instructions.filter(instruction => {
            return instruction.next == ins.next
        }).map(instruction => {
            return instruction.first;
        })

        steps.push({
            step: ins.next,
            followUps: [],
            requirements
        })
    }
})


// remove duplicate entries
steps = steps.filter((item, pos) => {
    return steps.findIndex(step => {
        return step.step === item.step
    }) === pos
})


let poss_steps = steps.filter(step => step.requirements.length === 0);
let finished = [];

// these are the workers
let processes = [];
for (let i =0; i < 5; i++) {
    processes.push({time: 0})
}

doStep(poss_steps, 0)

function doStep(possiblesteps) {

    // get the next steps, sorted
    possiblesteps.sort((a,b) => {
        return (a.step < b.step) ? -1 : (a.step > b.step) ? 1 : 0;
    })

    let nextSteps = possiblesteps.slice(processes.length, possiblesteps.length-1);
    possiblesteps.slice(0, processes.length).forEach(currentStep => {
        finished.push(currentStep);

        let shortestProcess = processes.sort((a,b) => {
            return a.time - b.time
        })[0]

        let shouldFinish = true;
        let timeToFinish = shortestProcess.time;
        currentStep.requirements.forEach(req => { 
            let result = steps.find(step => { 
                return step.step === req;
            })
            if (!result.finished) shouldFinish = false;
            if (result && result.finished && (result.finished > timeToFinish)) {
                timeToFinish = result.finished;
            } 
        }) 

        currentStep.finished = (timeToFinish + (60 + currentStep.step.charCodeAt(0) - 64))
        shortestProcess.time = currentStep.finished


        let followups = currentStep.followUps.map(followup => {
            let result = steps.find(step => { 
                return step.step === followup;
            })
            return result;      
        })


        followups.forEach(fu => {
            let meetsRequirements = !fu.requirements.some(req => { 
                return !finished.some(step => step.step === req);
            }) 

            if (meetsRequirements) {
                nextSteps.push(fu)
            }
        })
    })

    if (nextSteps.length > 0) {
        return doStep(nextSteps)
    } else {
        return;
    }

}

console.log("B: ", processes.sort((a,b) => {
    return b.time - a.time
})[0].time)


