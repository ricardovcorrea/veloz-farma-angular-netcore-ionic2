const Stores = {
    active: true,
    text: 'Lojas',
    link: '/stores',
    icon: 'icon-home',
    submenu: [
        {
            text: 'Lista',
            link: '/stores/list',
            active: true
        }
    ]
};

const Demands = {
    active: true,
    text: 'Demandas',
    link: '/demands',
    icon: 'icon-home',
    submenu: [
        {
            text: 'Lista',
            link: '/demands/all',
            active: true
        },
        {
            text: 'Nova demanda',
            link: '/demands/new',
            active: true
        },
        {
            text: 'Backlog',
            link: '/demands/backlog',
            active: true
        }
    ]
};

export const menu = [
    Stores
];

export const menu2 = [
    Demands
];
