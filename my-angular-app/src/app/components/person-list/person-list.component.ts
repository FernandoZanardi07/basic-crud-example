import { Component, OnInit } from '@angular/core';
import { MyService } from '../../services/my-service.service';

@Component({
    selector: 'app-person-list',
    templateUrl: './person-list.component.html',
    styleUrls: ['./person-list.component.scss']
})
export class PersonListComponent implements OnInit {
    persons: any[] = [];
    newPerson: any = { name: '', cpfNumber: '', dateOfBirth: Date, isActive: true };
    displayModal: boolean = false;
    phoneNumber: string = '';
    phoneType: number = 0;
    selectedPerson: any = null;
    selectedPersonId: string = '';
    contacts: any[] = [];

    constructor(private myService: MyService) { }

    ngOnInit(): void {
        this.loadPersons();
    }

    loadPersons(): void {
        this.myService.getPersons().subscribe(
            (data: any) => {
                this.persons = data.result.map((person: any) => {
                    person.dateOfBirth = new Date(person.dateOfBirth).toISOString().split('T')[0];
                    return person;
                });
            },
            (error) => {
                console.error('Error fetching persons', error);
            }
        );
    }

    savePerson(person: any): void {
        this.myService.updatePerson(person).subscribe(
            (data: any) => {
                console.log('Person updated:', data);
                this.loadPersons();
            },
            (error) => {
                console.error('Error updating person', error);
            }
        );
    }

    deletePerson(id: string): void {
        this.myService.deletePerson(id).subscribe(
            (data: any) => {
                console.log('Person deleted:', data);
                this.loadPersons();
            },
            (error) => {
                console.error('Error deleting person', error);
            }
        );
    }

    addPerson(): void {
        this.myService.addPerson(this.newPerson).subscribe(
            (data: any) => {
                console.log('Person added:', data);
                this.loadPersons();
                this.newPerson = { name: '', cpfNumber: '', dateOfBirth: '', isActive: true };
            },
            (error) => {
                console.error('Error adding person', error);
            }
        );
    }

    showContactModal(person: any): void {
        this.selectedPerson = person;
        this.selectedPersonId = person.id;
        this.loadContacts();
    }

    loadContacts(): void {
        this.myService.getContactsByPersonId(this.selectedPersonId).subscribe(
            (data: any) => {
                this.contacts = data;
                this.displayModal = true;
            },
            (error) => {
                console.error('Error fetching persons', error);
            }
        );
    }

    hideContactModal(): void {
        this.displayModal = false;
        this.phoneNumber = '';
    }

    addContact(): void {
        if (this.selectedPerson) {
            this.myService.addContact(this.selectedPersonId, { number: this.phoneNumber, type: this.phoneType }).subscribe(
                (data: any) => {
                    this.loadContacts();
                    this.phoneNumber = '';
                },
                (error) => {
                    console.error('Error adding contact', error);
                }
            );
            console.log('Contact added:', this.phoneNumber);
        }
    }

    deleteContact(contactId: string): void {
        if (this.selectedPerson) {
            this.myService.deleteContact(contactId).subscribe(
                (data: any) => {
                    this.loadContacts();
                },
                (error) => {
                    console.error('Error deleting contact', error);
                }
            );
        }
    }
}