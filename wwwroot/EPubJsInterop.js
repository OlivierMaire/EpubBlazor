console.log("I am here");

//  var tmp = document.querySelector("#epub-iframe");


// console.log(iframe.clientWidth);


// Select the node that will be observed for mutations
const targetNode = document.getElementById("epub-iframe");

// // Options for the observer (which mutations to observe)
// const config = { attributes: true, childList: true, subtree: true, characterData: true  };

// // Callback function to execute when mutations are observed
// const callback = (mutationList, observer) => {
//   for (const mutation of mutationList) {
//     if (mutation.type === "childList") {
//       console.log("A child node has been added or removed.");
//     } else if (mutation.type === "attributes") {
//       console.log(`The ${mutation.attributeName} attribute was modified.`);
//       console.log(targetNode.contentWindow.document.body.scrollWidth);
//     }
//     else{
//         console.log(`mutation ${mutation.type}`);
//     }
//   }
// };

// // Create an observer instance linked to the callback function
// const observer = new MutationObserver(callback);

// // Start observing the target node for configured mutations
// observer.observe(targetNode, config);

// // Later, you can stop observing
// observer.disconnect();


const resizeObserver = new ResizeObserver((entries) => {
 
    // requestAnimationFrame(()    => {
        
    // // const scrollWidth = document.body.scrollWidth;
    // const  scrollWidth = epubInterop.getScrollWidth();
    // console.log("Size changed " + scrollWidth);

    //  console.log('New scrollHeight', container.scrollHeight);
    // }

    console.log('New scrollHeight', container.scrollWidth);
}
    );
  
var container = targetNode.contentWindow.document.body;
// This is the critical part: We observe the size of all children!
for (const child of container.children) {
    resizeObserver.observe(child);
}

//   resizeObserver.observe();

export const epubInterop = {
    iframe :{},
    scroll :{},

    setElems: function (iframeElem, scrollElem) { this.iframe = iframeElem; this.scroll = scrollElem;},

    getIFrameSize: function () { return  {
       width: this.iframe.clientWidth,
       height: this.iframe.clientHeight}; },

    getScrollWidth: function () {
      if (this.iframe.contentWindow)
        return this.iframe.contentWindow.document.body.scrollWidth;
      else
        return 1000;
    },
    scrollTo: function (x) { 
      if (this.scroll.scrollTo){
      this.scroll.scrollTo(x, 0);} }
};