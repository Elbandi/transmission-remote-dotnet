#!/usr/bin/perl -w

my @file;   # file content

my $filename = $ARGV[0];
die "only for .cs files" unless ($filename =~ m/.cs$/i);

    open( BOMFILE, "<$filename" ) || die "Could not open source file for reading.";
    while (<BOMFILE>) {
                s/^\xEF\xBB\xBF//;
        push @file, $_ ;
	}
    close (BOMFILE)  || die "Can't close source file after reading.";

    open (NOBOMFILE, ">$filename") || die "Could not open source file for writing.";
    foreach $line (@file) {
        print NOBOMFILE $line;
        }
    close (NOBOMFILE)  || die "Can't close source file after writing.";
