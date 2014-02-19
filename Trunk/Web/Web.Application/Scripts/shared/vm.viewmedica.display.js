define('vm.viewmedica.display',
    ['ko', 'config', 'jquery'],
    function(ko, config, $) {


        function ViewMedicaDisplay() {
            this.animationTag = ko.observable();
        }

        ViewMedicaDisplay.prototype.init = function(animationCode, containerSelector) {
            $(containerSelector + ' .viewmedica-container').empty();
            
            if (animationCode !== null) {
                this.animationTag(animationCode);
                //hate this... reloads there player every time
                openthis = animationCode;
                vm_open();
            }  
        };

        return ViewMedicaDisplay;

    });