define('vm.admin.videos',
    ['ko', 'underscore', 'knockback', 'model.admin.video.collection', 'model.admin.video', 'error.helper', 'bootstrap.helper', 'config'],
    function (ko, _, kb, VideoCollection, VideoModel, err, bh, config) {

        var videoListTemplate = 'admin.video.list',
            videoCollection = new VideoCollection(),
            videos = kb.collectionObservable(videoCollection),
            selectedVideo = kb.viewModel(new VideoModel()),
            availableCategories = config.functionCategories,
            bindSelectedVideo = function (data, event) {
                selectedVideo.model(data.model());
                selectedVideo.categories(data.categories());
            },
            onSuccessfulChange = function () {
                selectedVideo.model(new VideoModel());
                videoCollection.fetch();
            },
            saveChanges = function () {
                selectedVideo.model().save({}, {
                    success: onSuccessfulChange, error: err.onError
                });
            };


        var videoValidationOptions = ko.observable({
            debug: true,
            ignore: config.kendoEditorIgnores,
            rules: {
                name:
                {
                    required: true,
                    minlength: 4,
                    maxlength: 100
                },
                medicalName:
                {
                    required: true,
                    minlength: 4,
                    maxlength: 100
                },
                fileName:
                {
                    required: true,
                    minlength: 5,
                    maxlength: 40
                },
                description:
                {
                    required: false,
                    minlength: 1,
                    maxlength: 60000
                },
                youtubeId:
                {
                    required: false,
                    minlength: 6,
                    maxlength: 20
                }
            },
            messages: {
                name:
                {
                    required: "name required",
                    minlength: "must be between 4 and 100 characters",
                    maxlength: "must be between 4 and 100 characters"
                },
                medicalName:
                {
                    required: "medical name required",
                    minlength: "must be between 4 and 100 characters",
                    maxlength: "must be between 4 and 100 characters"
                },
                fileName: {
                    required: "file name required",
                    minlength: "must be between 5 and 40 characters",
                    maxlength: "must be between 5 and 40 characters"
                },
                description:
                {
                    minlength: "must be between 1 and 60000 characters",
                    maxlength: "must be between 1 and 60000 characters"
                },
                youtubeId: {
                    minlength: "must be between 6 and 20 characters",
                    maxlength: "must be between 6 and 20 characters"
                }
            },
            submitHandler: saveChanges,
            errorPlacement: bh.popoverErrorPlacement('login-popover'),
            unhighlight: bh.popoverUnhighlight
        });

        var init = function() {
            videoCollection.fetch();
        };
        
        return {
            videos: videos,
            videoListTemplate: videoListTemplate,
            selectedVideo: selectedVideo,
            saveChanges: saveChanges,
            bindSelectedVideo: bindSelectedVideo,
            videoValidationOptions: videoValidationOptions,
            availableCategories : availableCategories,
            init: init,
            kendoEditorOptions: config.kendoEditorOptions
        };
    });