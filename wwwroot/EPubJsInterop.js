export const epubInterop = {
  iframe: {},
  scroll: {},

  setElems: function (iframeElem, scrollElem) { this.iframe = iframeElem; this.scroll = scrollElem; },

  getIFrameSize: function () {
    return {
      width: this.iframe.clientWidth,
      height: this.iframe.clientHeight
    };
  },

  getScrollWidth: function () {
    if (this.iframe.contentWindow)
      return this.iframe.contentWindow.document.body.scrollWidth;
    else
      return 1000;
  },
  scrollTo: function (x) {
    if (this.scroll.scrollTo) {
      this.scroll.scrollTo(x, 0);
    }
  }
};