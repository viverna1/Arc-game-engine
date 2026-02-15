class Menu {
    constructor() {
        const menuButtons = document.querySelectorAll('.menu-button');

        menuButtons.forEach(button => {
        button.addEventListener('click', (e) => {
            e.stopPropagation();
            
            // Закрыть все открытые меню
            menuButtons.forEach(btn => {
            if (btn !== button) {
                btn.nextElementSibling.style.display = 'none';
            }
            });
            
            // Переключить текущее меню
            const dropdown = button.nextElementSibling;
            dropdown.style.display = dropdown.style.display === 'block' ? 'none' : 'block';
        });
        });

        // Закрыть меню при клике вне
        document.addEventListener('click', () => {
            document.querySelectorAll('.dropdown').forEach(dropdown => {
                dropdown.style.display = 'none';
            });
        });
    }
}


class UI {
    constructor() {
        const configs = [
            { id: 'hierarchy-resizer', handler: 'hierarchyResizeHandle' },
            { id: 'inspector-resizer', handler: 'inspectorResizeHandle' },
            { id: 'bottom-panel-resizer', handler: 'bottomPanelResizeHandle' },
            { id: 'explorer-resizer', handler: 'explorerResizeHandle' },
            { id: 'files-hierarchy-resizer', handler: 'filesHierarchyResizeHandle' }
        ];

        document.body.style.setProperty('--explorer-width', (screen.width * 0.55) + 'px');
        
        configs.forEach(config => {
            const el = document.getElementById(config.id);
            const handler = this[config.handler].bind(this);
            el?.addEventListener('mousedown', handler);
        });
    }
    
    hierarchyResizeHandle(e) {
        this.startResize(e, '--hierarchy-width', this.calculateSize(10, 45), 1);
    }

    inspectorResizeHandle(e) {
        this.startResize(e, '--inspector-width', this.calculateSize(10, 45), -1);
    }

    bottomPanelResizeHandle(e) {
        this.startResize(e, '--bottom-panel-height', this.calculateSizeVertical(10, 40), -1, true);
    }
    
    explorerResizeHandle(e) {
        this.startResize(e, '--explorer-width', this.calculateSize(10, 70), 1);
    }
    
    filesHierarchyResizeHandle(e) {
        this.startResize(e, '--files-hierarchy-width', {min: 10, max: 400}, 1);
    }

    startResize(e, cssVar, size, direction, isVertical = false) {
        e.preventDefault();
        
        const startPos = isVertical ? e.clientY : e.clientX;
        const startValue = parseInt(getComputedStyle(document.body)
            .getPropertyValue(cssVar));

        const doResize = (moveEvent) => {
            const currentPos = isVertical ? moveEvent.clientY : moveEvent.clientX;
            const newValue = startValue + direction * (currentPos - startPos);
            const clampedValue = Math.min(Math.max(newValue, size.min), size.max);
            document.body.style.setProperty(cssVar, clampedValue + 'px');

            console.log(clampedValue);
            
            if (cssVar === '--explorer-width' && clampedValue < 300) {
                document.getElementById("files-hierarchy-resizer").style.display = 'none'
                document.body.style.setProperty("--files-hierarchy-width", clampedValue + 'px');
            } else {
                document.getElementById("files-hierarchy-resizer").style.display = 'block'
            }
        };

        const stopResize = () => {
            document.removeEventListener('mousemove', doResize);
            document.removeEventListener('mouseup', stopResize);
        };

        document.addEventListener('mousemove', doResize);
        document.addEventListener('mouseup', stopResize);
    }

    calculateSize(percentMin, percentMax) {
        return {
            min: screen.width * percentMin / 100,
            max: screen.width * percentMax / 100
        };
    }

    calculateSizeVertical(percentMin, percentMax) {
        return {
            min: screen.height * percentMin / 100,
            max: screen.height * percentMax / 100
        };
    }
}


class Explorer {
    constructor() {
        const nesteds = document.querySelectorAll('.nested');
        nesteds.forEach(nested => {
            nested.classList.toggle('collapsed');
        });

        const buttons = document.querySelectorAll('.file-tree-item.folder');
        
        buttons.forEach(button => {
            button.addEventListener('click', function() {
                const nextElement = this.nextElementSibling;
                if (nextElement && nextElement.classList.contains('nested')) {
                    nextElement.classList.toggle('collapsed');
                }
            });
        });
    }
}


class Scene {
    constructor() {
        this.data = {}
    }

    async Load() {
        const response = await fetch('scene.json');
        this.data = await response.json();
    }
}


async function main() {
    new Menu();
    new UI();
    new Explorer();
    
    const scene = new Scene();
    await scene.Load();

    console.log(scene.data);
    
}

main();
