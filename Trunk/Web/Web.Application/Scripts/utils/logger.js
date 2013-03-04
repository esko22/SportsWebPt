(function (global) {
    var self = this;

    this.history = [];

    this.log = function () {
        var msg = { args: arguments, time: self.timestamp() };
        self.history.push(msg);
        if (typeof console !== "undefined" && typeof console.log !== "undefined") {
            console.log(msg.time + ' - ' + Array.prototype.slice.call(arguments));
        }
        if (self.history.length > 200) {  //keep list small
            self.history = self.history.splice(0, 50);
        }
    };

    this.timestamp = function() {
        var a_p = "";
        var d = new Date();
        var currHour = d.getHours();
        var secs = d.getSeconds();
        var ms = d.getMilliseconds();
        if (currHour < 12) {
            a_p = "AM";
        }
        else {
            a_p = "PM";
        }
        if (currHour == 0) {
            currHour = 12;
        }
        if (currHour > 12) {
            currHour = currHour - 12;
        }
        var currMin = d.getMinutes();
        if (currMin < 10) {
            currMin = '0' + currMin;
        }
        if (secs < 10) {
            secs = '0' + secs;
        }
        return currHour + ":" + currMin + ":" + secs + "." + ms + " " + a_p;
    };
    //should this be 'this' or 'self' ??
    global.logger = this;
})(window);