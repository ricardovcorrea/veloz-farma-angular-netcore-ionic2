import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { NgModule } from '@angular/core';
import { HttpModule, Http } from '@angular/http';

import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { RoutesModule } from './routes/routes.module';

@NgModule({
    declarations: [
        AppComponent,
    ],
    imports: [
        HttpModule,
        BrowserAnimationsModule,
        CoreModule,
        SharedModule.forRoot(),
        RoutesModule
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
