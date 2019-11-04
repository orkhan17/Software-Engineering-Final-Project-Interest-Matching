import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberComponent} from './member/member.component';
import { MessageComponent} from './message/message.component';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            {path: 'member', component: MemberComponent},
            {path: 'message', component: MessageComponent},
        ]

    },
    {path: '**', redirectTo: '', pathMatch: 'full'}
];
