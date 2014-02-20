define('youtube.video.manager', ['jquery', 'underscore'], function($, _) {

    var videoPlayers = [];

    function addVideoInstance(videoId) {
        //id attr has to bind first... stinks...
        setTimeout(function () {
            var videoPlayer = new YT.Player(videoId, {
                events: {
                    'onReady': addVideo
                }
            });
        }, 1000);
    }

    function addVideo(event) {
        videoPlayers.push(event.target);
    }

    function destroyAll() {
        _.each(videoPlayers, function(videoPlayer) {
            videoPlayer.destroy();
        });

        videoPlayers.length = 0;
    }

    return {
        addVideoInstance: addVideoInstance,
        destroyAll : destroyAll
    };


});