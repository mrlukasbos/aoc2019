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
            requirements
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

doStep(poss_steps)

function doStep(possiblesteps) {

    let currentStep = possiblesteps.sort((a,b) => {
        return (a.step < b.step) ? -1 : (a.step > b.step) ? 1 : 0;
    })[0]        

    possiblesteps.splice(possiblesteps.indexOf(currentStep), 1);
    finished.push(currentStep);

    let followups = currentStep.followUps.map(followup => {
        let result = steps.find(step => { 
            return step.step === followup;
        })
        return result;      
    })

    let nextSteps = possiblesteps;

    followups.forEach(fu => {
        let meetsRequirements = !fu.requirements.some(req => { 
            return !finished.some(step => step.step === req);
        }) 

        if (meetsRequirements) {
            nextSteps.push(fu)
        }
    })


    if (nextSteps.length > 0) {
        return doStep(nextSteps)
    } else {
        console.log("done")
        return;
    }
}

console.log("A: ", finished.map(step => step.step).join(""))
