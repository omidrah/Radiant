import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-numeric-keypad',
  templateUrl: './numeric-keypad.component.html',
  styleUrls: ['./numeric-keypad.component.css']
})
export class NumericKeypadComponent {
  @Output() numberPressed = new EventEmitter<number>();
  @Output() backspacePressed = new EventEmitter<void>();
  //@Output() clearPressed = new EventEmitter<void>();
  @Output() enterPressed = new EventEmitter<void>();
  @Output() escPressed = new EventEmitter<void>();
  @Output() increasePressed = new EventEmitter<void>();
  @Output() decreasePressed = new EventEmitter<void>();
  @Output() decimalPressed = new EventEmitter<void>();

  appendNumber(number: number) {
    this.numberPressed.emit(number);
  }
  appendDecimal() {
    this.decimalPressed.emit();
  }
  backspace() {
    this.backspacePressed.emit();
  }

  // clear() {
  //   this.clearPressed.emit();
  // }
  enter() {
    this.enterPressed.emit();
  }

  esc() {
    this.escPressed.emit();
  }

  increase() {
    this.increasePressed.emit();
  }

  decrease() {
    this.decreasePressed.emit();
  }
}
