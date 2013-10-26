define('user.settings', ['jquery', 'config', 'model.user', 'knockback'],
    function($, config, User ,kb) {
        var user = new User(),
            currentUser = kb.viewModel(user)

        $(function () {
            var userId = $('#userid').val();

            if (userId > 0) {
                user.set({ 'id': userId });
                user.fetch();
                currentUser.model(user);
            }
        });

        return {
            currentUser: currentUser
        };
    });
