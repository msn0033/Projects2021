

import { animate, style, transition, trigger } from "@angular/animations";


export let slide = trigger('slide', [

    transition(':enter', [ // void=>*
        style({ transform: 'translateX(30px)' }),
        animate(550)
    ]),

    transition(':leave', [ //*=>void
        animate(2000, style({ transform: 'translateX(-100%)' }))
    ])
]);


export let fade = trigger('fade', [

    transition('void =>*', [ // :enter , :leave ====> void<=>*
        animate(2000, style({ backgroundColor: 'green', opacity: 1 }))
    ]),

    transition('* => void', [
        style({ backgroundColor: 'red', opacity: 0.5 }),
        animate(2000)
    ])
])