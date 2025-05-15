with Ada.Text_IO;              use Ada.Text_IO;
with Ada.Integer_Text_IO;      use Ada.Integer_Text_IO;
with Ada.Containers.Vectors;
with Ada.Numerics.Discrete_Random;

procedure Main is

   subtype Index_Type is Positive;

   package Int_Vec is new Ada.Containers.Vectors(Index_Type, Integer);
   use Int_Vec;

   A : Vector; --прибрати вектор

   Array_Size   : Integer;
   Thread_Count : Integer;

   protected Global_Min is
      procedure Update (Value : Integer; Index : Index_Type);
      function Get_Min return Integer;
      function Get_Index return Index_Type;
   private
      Min_Value : Integer := Integer'Last;
      Min_Index : Index_Type := 1;
   end Global_Min;

   protected body Global_Min is
      procedure Update (Value : Integer; Index : Index_Type) is
      begin
         if Value < Min_Value then
            Min_Value := Value;
            Min_Index := Index;
         end if;
      end Update;

      function Get_Min return Integer is
      begin
         return Min_Value;
      end Get_Min;

      function Get_Index return Index_Type is
      begin
         return Min_Index;
      end Get_Index;
   end Global_Min;

   task type Min_Finder(Start_Index, End_Index : Index_Type);

   task body Min_Finder is
      Local_Min   : Integer := Integer'Last;
      Local_Index : Index_Type := Start_Index;
   begin
   delay 10.0;
      for I in Start_Index .. End_Index loop
         if A(I) < Local_Min then
            Local_Min := A(I);
            Local_Index := I;
         end if;
      end loop;
      Global_Min.Update(Local_Min, Local_Index);
   end Min_Finder;

   package Rand_Int is new Ada.Numerics.Discrete_Random(Integer);
   Gen : Rand_Int.Generator;

begin
   Put("Enter array size: ");
   Get(Array_Size);
   Put("Enter number of threads: ");
   Get(Thread_Count);

   if Array_Size < Thread_Count then
      Put_Line("Thread count must be ≤ array size");
      return;
   end if;

   Rand_Int.Reset(Gen);
      for I in 1 .. Array_Size loop
      A.Append(Rand_Int.Random(Gen) mod 100_000);
   end loop;


   declare
      Neg_Idx : Index_Type := Index_Type(Rand_Int.Random(Gen) mod Array_Size + 1);
   begin
      A(Neg_Idx) := -(Rand_Int.Random(Gen) mod 1000);
      Put_Line("Inserted a negative number at position: " & Index_Type'Image(Neg_Idx));
   end;

   declare
      type Min_Finder_Access is access Min_Finder;
      type Finder_Array is array (Positive range <>) of Min_Finder_Access; --прибрати деклеа
      Finders : Finder_Array(1 .. Thread_Count);

      Part_Size : constant Integer := Array_Size / Thread_Count;
      Remainder : constant Integer := Array_Size mod Thread_Count;
      Start_Idx : Index_Type := 1;
   begin
      for T in 1 .. Thread_Count loop
         declare
            Extra   : constant Integer := (if T <= Remainder then 1 else 0);
            End_Idx : Index_Type := Start_Idx + Index_Type(Part_Size + Extra - 1);
         begin
            Finders(T) := new Min_Finder(Start_Idx, End_Idx);
            Start_Idx := End_Idx + 1;
         end;
      end loop;
      Put_Line("!!!!!!!!!!!!!!!!!!!!!1: ");
   end;

   Put_Line("Minimum value: " & Integer'Image(Global_Min.Get_Min));
   Put_Line("Index of minimum value: " & Index_Type'Image(Global_Min.Get_Index));

end Main;
