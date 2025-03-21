// ClientState example: 
// __clientState = {
//     "Deltas":[
//         {"Id":"0EM5i4S9","DeltaValueNames":["a"]}
//     ],
//     "DeltaValues":[
//         {"Name":"a","Value":2,"TypeName":"System.Int32"}
//     ]
// };


async function changeValue(name, value){
    // Set the value in the client state
    __clientState.DeltaValues.find(v => v.Name === name).Value = value;
    
    const activatedDeltas = __clientState.Deltas.filter(d => d.DeltaValueNames.includes(name));
    const deltaDependencies = activatedDeltas.map(d => d.DeltaValueNames).flat();
    const relatedValues = __clientState.DeltaValues.filter(v => deltaDependencies.includes(v.Name));
    
    let response = await fetch(window.location.pathname, {
        method: "POST",
        headers: {"Content-Type": "application/json", "X-Delta": "true"},
        body: JSON.stringify({Deltas: activatedDeltas, DeltaValues: relatedValues})
    });

    let clientActions = await response.json();
    for(let clientRender of clientActions.Renders){

        let elements = document.querySelectorAll('[data-delta-id="' + clientRender.DeltaId + '"]')
        if (elements.length === 0) {
            console.log("The dynamic html with id '" + htmlChange.Id + "' changed by '" + id + "' was not found")
        }
        for (let e of elements) {
            e.outerHTML = clientRender.Html;
        }
    }
}

async function changeDynamicValue(id, value, opts) {
    let valueChangeRequest = {...opts, Id: id, Value: value}
    let url = window.location.pathname + "?" + new URLSearchParams({ValueChangeRequest: 'true'});
    let response = await fetch(url, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify(valueChangeRequest)
    });

    let valueChangeRenders = await response.json();
    
    for(let jsonResult of valueChangeRenders){
        if (jsonResult?.Value) {
            proactCurrentValueMap[id] = jsonResult?.Value
        }
        for (let htmlChange of jsonResult.Changes) {
            let elements = document.querySelectorAll('[data-dynamic-value-id="' + htmlChange.Id + '"]')
            if (elements.length === 0) {
                console.log("The dynamic html with id '" + htmlChange.Id + "' changed by '" + id + "' was not found")
            }
            for (let e of elements) {
                e.outerHTML = htmlChange.Html;
            }
        }
    }
}