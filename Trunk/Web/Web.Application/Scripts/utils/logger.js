(function(global) {
    var logger = {
        history: [],

        log: function() {
            var msg = { args: arguments, time: this.timestamp() };
            this.history.push(msg);
            if (typeof console !== "undefined" && typeof console.log !== "undefined") {
                console.log(msg.time + ' - ' + Array.prototype.slice.call(arguments));
            }
            if (this.history.length > 200) { //keep list small
                this.history = this.history.splice(0, 50);
            }
        },

        timestamp: function() {
            var a_p = "";
            var d = new Date();
            var currHour = d.getHours();
            var secs = d.getSeconds();
            var ms = d.getMilliseconds();
            if (currHour < 12) {
                a_p = "AM";
            } else {
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
        }
    };
    global.logger = logger;
})(window);